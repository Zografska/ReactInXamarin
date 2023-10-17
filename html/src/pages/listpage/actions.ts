import { Action } from "redux"

export const REQUEST_LOAD_MORE_ITEMS = "REQUEST_LOAD_MORE_ITEMS"

export interface LoadMoreAction extends Action {
    listId: string
}

export const loadMoreItems = (listId: string): LoadMoreAction => ({
    type: REQUEST_LOAD_MORE_ITEMS,
    listId,
})

export const CHANGE_TITLE_COLOR = "CHANGE_TITLE_COLOR"

export interface ChangeTitleColor extends Action {
    color: string
}

export const changeTitleColor = (color: string): ChangeTitleColor => ({
    type: CHANGE_TITLE_COLOR,
    color,
})

export const RECEIVE_ITEMS = "RECEIVE_ITEMS"

export interface ReceiveItemsAction extends Action {
    items: Models.Item[],
}

export const receiveItems = (items: Models.Item[]): ReceiveItemsAction => ({
    type: RECEIVE_ITEMS,
    items,
})

export const NAVIGATE = "NAVIGATE"

export interface NavigateAction extends Action {
    itemId: number,
}

export const navigateToItemPage = (itemId: number): NavigateAction => ({
    type: NAVIGATE,
    itemId,
})

export const mapDispatchToProps = {
    loadMoreItems,
    navigateToItemPage,
    changeTitleColor,
}

export type DispatchProps = typeof mapDispatchToProps
