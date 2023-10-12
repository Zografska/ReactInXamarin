import { Action } from "redux";
import * as actions from "./actions";

export const initialState: ListPage.ListPage = {
    items: [],
    isLoading: false,
    canLoadMore: true,
    currentPage: 0,
    listId: "",
    searchText: "",
}

export default function reducer(
    state = initialState,
    action: Action,
): ListPage.ListPage {
    switch (action.type) {
        case actions.REQUEST_LOAD_MORE_ITEMS:
            const { searchText } = action as actions.LoadMoreAction;
            return { ...state, isLoading: true, searchText};

        case actions.RESET_PAGING:
            return { ...state, items: [], isLoading: true, canLoadMore: true, currentPage: 1 }

        case actions.RECEIVE_ITEMS:
            const { items: itemsArray, page } = action as actions.ReceiveItemsAction;
            const recievedItems: Models.Item[] = page === 1 ? itemsArray : (state.items as Models.Item[]).concat(itemsArray);
            const isLoading = false;
            return { ...state, items: recievedItems, isLoading, currentPage: page }

        case actions.INITIALIZE:
            const { listId, page: currentPage } = action as actions.InitializeAction;
            return { ...state, listId, currentPage}

        case actions.RELOAD:
            return { ...state, isLoading: true}

        default:
            return state
    }
}