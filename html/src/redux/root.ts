import { combineEpics } from "redux-observable"
import { combineReducers } from "redux"
import { GlobalState } from "./global"

import ListPageEpics from "../pages/listpage/epics"
import ListPageReducer from "../pages/listpage/reducer"

export const rootEpic = combineEpics(ListPageEpics)

export const rootReducer = combineReducers<GlobalState>({
    listPage: ListPageReducer
})
