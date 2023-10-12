declare namespace Models {
    interface Item {
        id: number
        listId: number
        title: string
        listName: string
        description: string
    }

    interface ListItemsResponse {
        items: Item []
        canLoadMore: boolean
    }
}