// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

open System
open System.Runtime.InteropServices


#load @"..\src\SwiBridge\ApiStubs.fs"
#load @"..\src\SwiBridge\PrimitiveApi.fs"
#load @"..\src\SwiBridge\Easy.fs"
open SwiBridge.ApiStubs
open SwiBridge.PrimitiveApi
open SwiBridge.Easy


let floor2a () : int = 
    let fid = plOpenForeignFrame () 
    let goal = plNewTermRef ()
    let a1 = plNewTermRef ()
    let a2 = plNewTermRef ()
    let f1 = plNewFunctor (plNewAtom "floor") 2
    
    plPutFloat a1 12.45 |> ignore
    PL_cons_functor2(getTermT goal, getFunctorT f1, getTermT a1, getTermT a2) |> ignore
    plCall goal ModuleT.Zero |> ignore

    let ans :IntPtr = new IntPtr(0)
    let errno = PL_get_integer(getTermT a2, ans)
    printfn "errno: %i" errno

    plDiscardForeignFrame fid
    ans.ToInt32()


let floor2b () : int = 
    let fid = plOpenForeignFrame () 
    let goal = plNewTermRef ()
    let a1 = plNewTermRef ()
    let a2 = plNewTermRef ()
    let f1 = plNewFunctor (plNewAtom "floor") 2
    
    plPutFloat a1 12.45 |> ignore
    PL_cons_functor2(getTermT goal, getFunctorT f1, getTermT a1, getTermT a2) |> ignore
    plCall goal ModuleT.Zero |> ignore

    let mutable ans = 0
    let errno = PL_get_integerZ(getTermT a2, &&ans)
    printfn "errno: %i" errno

    plDiscardForeignFrame fid
    ans


let test01 () = 
    let code = PL_initialise(3, [| "./"; "-q"; "-nosignals" |])
    printfn "code: %i" code
    printfn "floor: %i" (floor2a ())

let test02 () = 
    let code = PL_initialise(3, [| "./"; "-q"; "-nosignals" |])
    printfn "code: %i" code
    printfn "floor: %i" (floor2b ())    
