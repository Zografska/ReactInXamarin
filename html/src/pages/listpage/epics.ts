import { combineEpics, ofType, Epic } from "redux-observable";
import { GlobalDependencies } from "redux/epicsDependencies";
import { of, from, concat } from "rxjs"
import { concatMap, mergeMap} from "rxjs/operators"

import * as actions from "./actions"

export const requestLoadMoreItemsEpic: Epic = (
    action$, 
    _state$, 
    {listPage: {itemAPI}}: GlobalDependencies
) =>
    action$.pipe(
        ofType(actions.REQUEST_LOAD_MORE_ITEMS),
        concatMap((action: actions.LoadMoreAction) =>
            from(
                itemAPI.getItems(
                    action.listId
                )
            ).pipe(
                mergeMap((response: Models.Item[]) => concat(
                        of(actions.receiveItems(response)))),
            )
        )
    )

export const navigateEpic: Epic = (action$, _, {listPage: {itemAPI}}: GlobalDependencies) =>
    action$.pipe(
        ofType(actions.NAVIGATE),
        concatMap((action: actions.NavigateAction) => concat(
            from(
                itemAPI.navigateToItem(action.itemId)
                )
            ),
        )
    )

export default combineEpics(
    requestLoadMoreItemsEpic,
    navigateEpic,
)