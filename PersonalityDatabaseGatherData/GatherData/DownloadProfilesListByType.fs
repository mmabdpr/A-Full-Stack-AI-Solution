module GatherData.DownloadProfilesListByType

open System.IO
open FSharp.Data

open GatherData.Common
open MongoDB.Bson
open MongoDB.Driver

[<CLIMutable>]
type PTypeDataItem = {_id: BsonObjectId; pType: string; data: string; }

let private db =
    mongoClient.GetDatabase("PersonalityData")
    
let private types =
    db.GetCollection<PTypeDataItem>("PTypesData")

let private savePersonalityDataToFile (pType: string, data: string) =
    if not (Directory.Exists(dataDir)) then
        Directory.CreateDirectory(dataDir) |> ignore

    let path = getPTypeFilePath pType

    Logger.logInfo(sprintf "saving %s into %s" pType path)
    let json = JsonValue.Parse(data)
    File.WriteAllText(path, json.ToString())

let private downloadPersonalityData (pType: string) =
    let url =
        "https://api.personality-database.com/api/v1/search_by_type"

    let data =
        sprintf """
    {
        "personality_type": "%s",
        "is_web": true,
        "per_page": "100000",
        "next_offset": 0
    }
    """  pType

    let body = HttpRequestBody.TextRequest data
    let response = Http.AsyncRequestString(url, httpMethod = "POST", body = body)
                   |> Async.Catch
                   |> Async.RunSynchronously
    match response with
    | Choice1Of2 text ->
        types.InsertOneAsync({_id = null; pType = pType; data = text;})
        |> Async.AwaitTask
        |> Async.RunSynchronously
        Logger.logInfo(sprintf "got response from %s" pType)
        savePersonalityDataToFile (pType, text)
    | Choice2Of2 e -> 
        Logger.logError(sprintf "error when downloading data for type %s\nerror: \n%A\n" pType e)
        Logger.logError(sprintf "%A" e)
        
let private downloadPersonalityDataV2 (pType: string, pTypeId: string) =
    let url =
        sprintf "https://api.personality-database.com/api/v1/search_by_type/%s?&is_web=true&per_page=100000&next_offset=0"
            pTypeId

    let response = Http.AsyncRequestString(url, httpMethod = "GET")
                   |> Async.Catch
                   |> Async.RunSynchronously
    match response with
    | Choice1Of2 text ->
        types.InsertOneAsync({_id = null; pType = pType; data = text;})
        |> Async.AwaitTask
        |> Async.RunSynchronously
        Logger.logInfo(sprintf "got response from %s" pType)
        savePersonalityDataToFile (pType, text)
    | Choice2Of2 e -> 
        Logger.logError(sprintf "error when downloading data for type %s\nerror: \n%A\n" pType e)
        Logger.logError(sprintf "%A" e)


let startDownload () =
    personality2Id
    |> Seq.map (fun p -> async { downloadPersonalityDataV2(p.Key, p.Value) })
    |> (fun it -> Async.Parallel(it, maxDegreeOfParallelism=8))
    |> Async.RunSynchronously
    |> ignore
