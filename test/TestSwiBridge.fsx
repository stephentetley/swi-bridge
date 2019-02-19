// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

open System
open System.Runtime.InteropServices
open System.Threading
open Microsoft.FSharp.NativeInterop

#load @"..\src\SwiBridge\ApiStubs.fs"
#load @"..\src\SwiBridge\PrimitiveApi.fs"
open SwiBridge.ApiStubs
open SwiBridge.PrimitiveApi

let test01 () = 
    let code = PL_initialise(3, [| "./"; "-q"; "-nosignals" |])
    printfn "code: %i" code
    let fid = plOpenForeignFrame() 
    let term1 = PL_new_term_ref()
    let ans1 = PL_chars_to_term("write(\"hello world!\").", term1)
    printfn "ans1: %i" ans1
    let ans2 = PL_call(term1, IntPtr.Zero)
    printfn "ans2: %i" ans2
    Thread.Sleep(3600)
    plDiscardForeignFrame(fid)
    PL_halt 1
