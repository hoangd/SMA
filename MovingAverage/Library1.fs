
namespace MovingAverage

open System.Collections.Generic
open System

type MovingAverageSettings  = {Days:int}




type SimpleMovingAverageCalculator (setting:MovingAverageSettings) = 
    member x.Calculate(data:float seq):float seq = 
        let days = setting.Days
        if data |> Seq.length < days then Seq.empty
        else 
            let ary = new LinkedList<float>()
            let result = new ResizeArray<float>(Seq.length data)
            let state = (ary,result)
            let minCount = days-1
            let (k,res) =data |> Seq.fold (fun (items:LinkedList<float>,result:ResizeArray<float>) e ->
                                            
                                            if items.Count < minCount then 
                                                items.AddLast(e) |>ignore
                                                (items,result)
                                            else 
                                                items.AddLast(e) |>ignore
                                                let avg = items |> Seq.average 
                                                result.Add(avg)
                                                items.RemoveFirst()
                                                (items,result)
                                            ) state
            res :> float seq 