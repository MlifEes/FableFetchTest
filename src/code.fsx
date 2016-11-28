#r "../node_modules/fable-core/Fable.Core.dll"
#r "../node_modules/fable-powerpack/Fable.PowerPack.dll"

open System
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.PowerPack
open Fable.PowerPack.Fetch

let allwaysFail<'T> reason =
    JS.Promise.reject<'T> reason

type MyRecord = { 
    userId : int
    id: int
    title: string
    body: string
 }
let jsonUrlWorking = "https://jsonplaceholder.typicode.com/posts/1"
let jsonUrlFailing = "https://jsonplaceholder.typicode.com/aaaabbs"

let showResult elementId x =
    let element = Browser.document.getElementById elementId
    element.innerText <- (sprintf "Hello the title is: %A" x)
    ()


let getInfo jsonUrl presenter =
    fetchAs<MyRecord> jsonUrl  []
    |> Promise.map (fun resp -> resp.title)
    |> Promise.catch(fun _ -> "failed")
    |> Promise.iter presenter 

"failing"
|> showResult
|> getInfo jsonUrlFailing

"working"
|> showResult
|> getInfo jsonUrlWorking