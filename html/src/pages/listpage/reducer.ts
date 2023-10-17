import { Action } from "redux";
import * as actions from "./actions";

export const initialState: ListPage.ListPage = {
    items: [],
    isLoading: false,
    listId: "",
    titleColor: "black"
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

        case actions.CHANGE_TITLE_COLOR:
            const { color } = action as actions.ChangeTitleColor
            return { ...state, titleColor: color }
            
        default:
            return state
    }
}