// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

#load @"..\src\SwiBridge\ApiStubs.fs"
#load @"..\src\SwiBridge\PrimitiveApi.fs"
open SwiBridge.PrimitiveApi

// SWI Prolog Manual page 375

let main (args: string list) : int = 
    if not (plIinitialise args) then
        plHalt(1)
    else 
        plHalt(if plToplevel () then 0 else 1)


let test01 () : int = main ["./"; "-q"; "-nosignals" ]
