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
            return { ...state, isLoading: true};

        case actions.RECEIVE_ITEMS:
            const { items } = action as actions.ReceiveItemsAction;
            return { ...state, items, isLoading: false }

        default:
            return state
    }
}