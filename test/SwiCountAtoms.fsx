// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

open System
open System.Runtime.InteropServices


#load @"..\src\SwiBridge\ApiStubs.fs"
#load @"..\src\SwiBridge\PrimitiveApi.fs"
open SwiBridge.ApiStubs
open SwiBridge.PrimitiveApi


// This is Figure 11.3 in the SWI Prolog manual, p 359.

// statistics(atoms,X).
let test01 () = 
    let code = PL_initialise(3, [| "./"; "-q"; "-nosignals" |])
    printfn "code: %i" code
    let fid = plOpenForeignFrame () 
    let goal = plNewTermRef ()
    let a1 = plNewTermRef ()
    let a2 = plNewTermRef ()
    let s2 = plNewFunctor (plNewAtom "statistics") 2
    

    plPutAtomChars a1 "atoms" |> ignore
    plConsFunctor2 goal s2 a1 a2 |> ignore
    plCall goal ModuleT.Zero |> ignore

    let ans:IntPtr = new IntPtr(0)
    let errno = PL_get_integer(getTermT a2, ans)
    printfn "errno: %i" errno
    let count = ans.ToInt32() 

    plDiscardForeignFrame fid

    count
   

    

