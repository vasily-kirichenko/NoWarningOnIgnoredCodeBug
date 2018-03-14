open Akka
open Akka.Actor
open Akka.Streams
open Akka.Streams.Dsl
open Akkling
open Akkling.Streams
open Akkling.Streams.Operators
open System

type IUser = interface end
type IHashtagEntity = interface end

type ITweet = 
    abstract Hashtags: IHashtagEntity list
    abstract CreatedBy: IUser

[<EntryPoint>]
let main _ = 
    use system = ActorSystem.Create("test")
    let mat = system.Materializer()
    
    GraphDsl.Create (fun b ->
        let broadcast = b.Add(Broadcast<ITweet>(2))
        ClosedShape.Instance
    )
    |> RunnableGraph.FromGraph
    |> Graph.run mat
    
    0
