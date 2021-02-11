module GatherData.Logger

open GatherData.Common
open MongoDB.Bson

[<CLIMutable>]
type LogItem = { Id : BsonObjectId; Text: string; }

let private db =
    mongoClient.GetDatabase("PersonalityLog")
    
let private errorCollection =
    db.GetCollection<LogItem>("errors")
    
let private infoCollection=
    db.GetCollection<LogItem>("info")

let logError (error: string) =
    errorCollection.InsertOne({Id = null; Text = error})
    
let logInfo(info: string) =
    infoCollection.InsertOne({Id = null; Text = info})
