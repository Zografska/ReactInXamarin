import {
    combineEpics,
    createEpicMiddleware,
    StateObservable
} from "redux-observable"

import { Observable } from "rxjs"
import { Action } from "redux"

import listPageEpics from "../pages/listpage/epics"

import { itemAPI,  } from "../apis"

import { GlobalState } from "./global"

interface ListPageDependencies {
    itemAPI: typeof itemAPI
}

export interface GlobalDependencies {
listPage: ListPageDependencies
}

export const globalDependencies: GlobalDependencies = {
    listPage: {
        itemAPI,
    }
}

export declare interface Epic<
    Input extends Action = Action,
    Output extends Input = Input,
    State = GlobalState,
    Dependencies = GlobalDependencies
> {
    (
        action$: Observable<Input>,
        state$: StateObservable<State>,
        dependencies: Dependencies,
    ): Observable<Output>
}

export const rootEpic = combineEpics(
    listPageEpics,
)

export const epicMiddleware = createEpicMiddleware<
    Action,
    Action,
    GlobalState,
    GlobalDependencies
>({ dependencies: globalDependencies })

export default epicMiddleware
