import { Subject } from "rxjs"
import { StateObservable } from "redux-observable"
import { GlobalState } from "../redux/global"

import { initialState as listPageState } from "./listpage/reducer"

const clone = jest.requireActual("rfdc")({ proto: true, circles: false })

// for selectors that need the whole state
export const initialGlobalState: GlobalState = {
    listPage: listPageState
}

// for epics
export const getInitialState$ = (): StateObservable<GlobalState> =>
    new StateObservable<GlobalState>(
        new Subject<GlobalState>(),
        // Some tests modify the global state. When that happens, it will cause other tests to break.
        // To make sure everything starts with a clean state, we create a deep copy of the global state.
        // As our global state has plenty of nested objects, we use a separate library for this
        // The chosen package is: rfdc (Really Fast Deep Clone) https://github.com/davidmarkclements/rfdc
        clone(initialGlobalState)
    )
