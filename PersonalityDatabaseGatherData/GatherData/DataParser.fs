module GatherData.DataParser

open System.IO
open FSharp.Data

open GatherData.Common

type PersonalityCard = JsonProvider<"../Data/ESTJ.json">

let getProfilesIds (pType: string) : seq<string> =
    let dataPath = getPTypeFilePath pType
    let data = File.ReadAllText(dataPath)
    (PersonalityCard.Parse data).Items
    |> Seq.map (fun it -> it.Id.ToString())
