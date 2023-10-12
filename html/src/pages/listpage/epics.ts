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
                    action.listId,
                    action.page,
                    action.searchText,
                )
            ).pipe(
                mergeMap((response: Models.Item[]) => concat(
                        of(actions.receiveItems(response, action.page)))),
            )
        )
    )

export const reloadEpic: Epic = (action$, state$) =>
    action$.pipe(
        ofType(actions.RELOAD),
        concatMap(() => concat(
            of(actions.resetPaging()),
            of(actions.loadMoreItems(state$.value.listPage.listId, 1, state$.value.listPage.searchText, true)))
        ),
    )

export default combineEpics(
    requestLoadMoreItemsEpic,
    reloadEpic,
)