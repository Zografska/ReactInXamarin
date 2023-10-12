declare namespace ListPage {
    interface ListPage {
        items: Items[]
        isLoading: boolean
        canLoadMore: boolean
        currentPage: number
        listId: string
        searchText: string
    }
}