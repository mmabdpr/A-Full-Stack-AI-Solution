module GatherData.Common

open System
open System.IO
open MongoDB.Driver

let personalities = seq [
    "ISTJ";
    "ESTJ";
    "ISFJ";
    "ESFJ";
    "ESFP";
    "ISFP";
    "ESTP";
    "ISTP";
    "INFJ";
    "ENFJ";
    "INFP";
    "ENFP";
    "INTP";
    "ENTP";
    "INTJ";
    "ENTJ";
]
    
let personality2Id = readOnlyDict [
    "ISTJ", "1";
    "ESTJ", "2";
    "ISFJ", "3";
    "ESFJ", "4";
    "ESFP", "5";
    "ISFP", "6";
    "ESTP", "7";
    "ISTP", "8";
    "INFJ", "9";
    "ENFJ", "10";
    "INFP", "11";
    "ENFP", "12";
    "INTP", "13";
    "ENTP", "14";
    "INTJ", "15";
    "ENTJ", "16";
]

let id2Personality = readOnlyDict [
    "1", "ISTJ";
    "2", "ESTJ";
    "3", "ISFJ";
    "4", "ESFJ";
    "5", "ESFP";
    "6", "ISFP";
    "7", "ESTP";
    "8", "ISTP";
    "9", "INFJ";
    "10", "ENFJ";
    "11", "INFP";
    "12", "ENFP";
    "13", "INTP";
    "14", "ENTP";
    "15", "INTJ";
    "16", "ENTJ";
]

let dataDir = Path.Combine(AppContext.BaseDirectory, "Data")

let getPTypeFilePath (pType: string) =
    Path.Combine(dataDir, (sprintf "%s.json" pType))
    
let mongoClient =
    MongoClient("mongodb://localhost:27017")