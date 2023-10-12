import { Action } from "redux"

export const REQUEST_LOAD_MORE_ITEMS = "REQUEST_LOAD_MORE_ITEMS"

export interface LoadMoreAction extends Action {
    listId: string
    page: number,
    searchText: string,
    shouldResetData: boolean
}

export const loadMoreItems = (listId: string, page: number, searchText: string, shouldResetData: boolean): LoadMoreAction => ({
    type: REQUEST_LOAD_MORE_ITEMS,
    listId,
    page,
    searchText,
    shouldResetData
})

export const RELOAD = "LIST_PAGE_RELOAD"

export const reload = (): Action => ({
    type: RELOAD,
})

export const RESET_PAGING = "RESET_PAGING"

export const resetPaging = (): Action => ({
    type: RESET_PAGING,
})

export const RECEIVE_ITEMS = "RECEIVE_ITEMS"

export interface ReceiveItemsAction extends Action {
    items: Models.Item[],
    page: number
}

export const receiveItems = (items: Models.Item[], page: number): ReceiveItemsAction => ({
    type: RECEIVE_ITEMS,
    items,
    page
})

export const INITIALIZE = "LIST_PAGE_INITIALIZE"

export interface InitializeAction extends Action {
    listId: string,
    page: number,
    searchText: string
}

export const initialize = (listId: string, page: number, searchText: string): InitializeAction => ({
    type: INITIALIZE,
    listId,
    page,
    searchText
})

export const mapDispatchToProps = {
    loadMoreItems,
    resetPaging,
    initialize,
}

export type DispatchProps = typeof mapDispatchToProps
