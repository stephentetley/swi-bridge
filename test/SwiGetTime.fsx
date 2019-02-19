// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

open System
open System.Runtime.InteropServices


#load @"..\src\SwiBridge\ApiStubs.fs"
#load @"..\src\SwiBridge\PrimitiveApi.fs"
open SwiBridge.ApiStubs
open SwiBridge.PrimitiveApi


let getTime () : double = 
    let fid = plOpenForeignFrame () 
    let goal = plNewTermRef ()
    let a1 = plNewTermRef ()
    let f1 = plNewFunctor (plNewAtom "get_time") 1
    

    plConsFunctor1 goal f1 a1 |> ignore
    plCall goal ModuleT.Zero |> ignore

    let mutable ans:double = 0.0;
    let errno = PL_get_float(getTermT a1, &&ans)
    printfn "errno: %i" errno

    plDiscardForeignFrame fid
    ans

// get_time(X).
let test01 () = 
    let code = PL_initialise(3, [| "./"; "-q"; "-nosignals" |])
    printfn "code: %i" code
    printfn "get_time: %f" (getTime ())

    
