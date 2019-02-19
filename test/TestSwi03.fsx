﻿// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

open System.Threading

#load @"..\src\SwiBridge\ApiStubs.fs"
#load @"..\src\SwiBridge\PrimitiveApi.fs"
#load @"..\src\SwiBridge\Easy.fs"
open SwiBridge.ApiStubs
open SwiBridge.PrimitiveApi
open SwiBridge.Easy


// This is PySWIP's test_create_term

let test01 () = 
    let code = PL_initialise(3, [| "./"; "-q"; "-nosignals" |])
    printfn "code: %i" code
    let fid = plOpenForeignFrame() 

    let arrT : TermT [] = plNewTermRefs 2
    let a1 = arrT.[0]
    let a2 = arrT.[1]
    let t = plNewTermRef ()
    let ta = plNewTermRef ()

    let animal2 = plNewFunctor (plNewAtom "animal") 2
    let assertz = plNewFunctor (plNewAtom "assertz") 1

    plPutAtomChars a1 "gnu"         |> ignore
    plPutInteger a2 51              |> ignore
    plConsFunctorV t animal2 a1     |> ignore
    plConsFunctorV ta assertz t     |> ignore
    plCall ta ModuleT.Zero          |> ignore

    Thread.Sleep(3600)
    plDiscardForeignFrame(fid)
    PL_halt 1


    