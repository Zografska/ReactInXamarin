/* eslint @typescript-eslint/no-explicit-any: "off" */

import * as baseAPI from "./base"

export declare namespace Interfaces {
    interface PaginatedResponse<T> {
        data: T[]
        paging: Pagination
    }

    interface Pagination {
        pageSize: number
        totalRecordCount: number
    }
}

const apiPath = "api"

export const getJSON = (
    path: string,
    args?: any,
    receiver?: Api.Receiver,
): Promise<any> =>
    baseAPI.getJSON(
        `${apiPath}/${path}`,
        args,
        new Headers(),
        receiver,
    )

export const getText = (
    path: string,
    args?: any,
    receiver?: Api.Receiver,
): Promise<any> =>
    baseAPI.getText(
        `${apiPath}/${path}`,
        args,
        new Headers(),
        receiver,
    )

export const postJSON = (
    path: string,
    args?: any,
    receiver?: Api.Receiver,
): Promise<any> =>
    baseAPI.postJSON(
        `${apiPath}/${path}`,
        args,
        new Headers(),
        receiver,
    )

export const putJSON = (
    path: string,
    args?: any,
    receiver?: Api.Receiver,
): Promise<any> =>
    baseAPI.putJSON(
        `${apiPath}/${path}`,
        args,
        new Headers(),
        receiver,
    )

export const remove = (
    path: string,
    args?: any,
    receiver?: Api.Receiver,
): Promise<any> =>
    baseAPI.remove(
        `${apiPath}/${path}`,
        args,
        new Headers(),
        receiver,
    )
