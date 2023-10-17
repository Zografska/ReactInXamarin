import { Action } from "redux"

export const REQUEST_LOAD_MORE_ITEMS = "REQUEST_LOAD_MORE_ITEMS"

export interface LoadMoreAction extends Action {
    listId: string
}

export const loadMoreItems = (listId: string): LoadMoreAction => ({
    type: REQUEST_LOAD_MORE_ITEMS,
    listId,
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
}

export type DispatchProps = typeof mapDispatchToProps
