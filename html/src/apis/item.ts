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
    listId: string,
): Promise<Models.Item[]> =>
{    
    return mobileAPI
        .getJSON(`items/${listId}`)
        .then((response: ItemApi.ItemDTO[]) => {
            const items = response.map(convertToItem)
            return items
        })}

export const navigateToItem = (
    itemId: number,
): Promise<void> =>
{    
    return mobileAPI
        .getJSON(`items/navigate/${itemId}`)
        .then((response: string) => {
            if(response !== "OK")
            {
                throw new Error("Something went wrong when navigating")
            }
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
