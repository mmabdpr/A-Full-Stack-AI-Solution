module GatherData.DownloadProfilesDetailsById

open System.Net
open FSharp.Data

open GatherData.Common
open GatherData.DataParser


let private db =
    mongoClient.GetDatabase("PersonalityData")
    
let private profiles =
    db.GetCollection<obj>("ProfilesById")

let private downloadProfileById (id: string) =
    Logger.logInfo(sprintf "downloading profile of id %s\r" id)
    let url = sprintf "https://api.personality-database.com/api/v1/profile/%s?version=W2" id
    let response = Http.AsyncRequestString(url, httpMethod = "POST")
                   |> Async.Catch
                   |> Async.RunSynchronously
    match response with
    | Choice1Of2 text -> Logger.logInfo(sprintf "got profile %s" id)
                         profiles.InsertOne(text)

//                         let filePath = Path.Combine(dataDir, (sprintf "profile_%s.json" id))
//                         Logger.logInfo(sprintf "writing to %s" filePath)
//                         File.WriteAllText(filePath, text)
    | Choice2Of2 e -> Logger.logError(sprintf "error downloading profile id %s" id)
                      Logger.logError(sprintf "%A" e)
                      
        

let private downloadAllProfilesByType (pType: string) =
    Logger.logInfo(sprintf "downloading profiles of type %s" pType)
    getProfilesIds pType
    |> Seq.map (fun id -> async { downloadProfileById id })
    |> (fun it -> Async.Parallel(it, maxDegreeOfParallelism=4))
    |> Async.RunSynchronously
    |> ignore

let startDownload () =
    personalities
    |> Seq.map (fun p -> async { downloadAllProfilesByType p })
    |> (fun it -> Async.Parallel(it, maxDegreeOfParallelism=2))
    |> Async.RunSynchronously
    |> ignore
    
// TODO clean up this file .. correct mongo inserts
// TODO correct format for all data insertions to mongo
// TODO remove all save to file codes
// TODO get Ids from mongo