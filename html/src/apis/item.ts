import { mobileAPI } from "./api"

declare namespace ItemApi {
    export interface ItemDTO {
        Id: number
        ListId: number
        Title: string
        ListName: string
        Description: string
    }
}

export const getItems = (
    listId: string, page: number, searchText: string
): Promise<Models.Item[]> =>
{    
    return mobileAPI
        .getJSON(`items/${listId}`)
        .then((response: ItemApi.ItemDTO[]) => {
            const items = response.map(convertToItem)
            return items
        })}


const convertToItem = (item: ItemApi.ItemDTO): Models.Item => {
    return {
        id: item.Id,
        listId: item.ListId,
        title: item.Title,
        listName: item.ListName,
        description: item.Description
    }
}
