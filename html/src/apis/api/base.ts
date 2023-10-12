/* eslint @typescript-eslint/no-explicit-any: "off" */

export const get = (
    path: string,
    args: any,
    headers: Headers = new Headers(),
    receiver: Api.Receiver,
): Promise<any> => {
    const api: Api.Api = {
        args,
        headers,
        method: "GET",
        path,
        receiver,
    }
    return createRequest(api)
}

export const getJSON = (
    path: string,
    args?: any,
    headers?: Headers,
    receiver: Api.Receiver = ReceiveJSON,
): Promise<any> => {
    return get(path, args, headers, receiver)
}

export const getText = (
    path: string,
    args?: any,
    headers?: Headers,
    receiver: Api.Receiver = ReceiveText,
): Promise<any> => {
    return get(path, args, headers, receiver)
}

export const getXML = (
    path: string,
    args?: any,
    headers?: Headers,
    receiver: Api.Receiver = ReceiveXML,
): Promise<any> => {
    return get(path, args, headers, receiver)
}

export const post = (
    path: string,
    args: any,
    headers: Headers = new Headers(),
    receiver: Api.Receiver,
): Promise<any> => {
    const api: Api.Api = {
        args,
        method: "POST",
        path,
        headers,
        receiver,
    }
    return createRequest(api)
}

export const postHTML = (
    path: string,
    args?: any,
    headers: Headers = new Headers(),
    receiver: Api.Receiver = ReceiveJSON,
): Promise<any> => {
    headers.append("Content-Type", "text/html; charset=UTF-8")
    return post(path, args, headers, receiver)
}

export const postJSON = (
    path: string,
    args?: any,
    headers: Headers = new Headers(),
    receiver: Api.Receiver = ReceiveJSON,
): Promise<any> => {
    headers.append("Content-Type", "application/json; charset=UTF-8")
    return post(path, JSON.stringify(args), headers, receiver)
}

export const postText = (
    path: string,
    args?: any,
    headers: Headers = new Headers(),
    receiver: Api.Receiver = ReceiveJSON,
): Promise<any> => {
    headers.append("Content-Type", "text/plain; charset=UTF-8")
    return post(path, args, headers, receiver)
}

export const postXML = (
    path: string,
    args?: any,
    headers: Headers = new Headers(),
    receiver: Api.Receiver = ReceiveJSON,
): Promise<any> => {
    headers.append("Content-Type", "text/xml; charset=UTF-8")
    return post(path, args, headers, receiver)
}

export const postVoid = (
    path: string,
    args?: any,
    headers: Headers = new Headers(),
    receiver: Api.Receiver = ReceiveNothing,
): Promise<any> => {
    headers.append("Content-Type", "*/*; charset=UTF-8")
    return post(path, args, headers, receiver)
}

export const put = (
    path: string,
    args: any,
    headers: Headers = new Headers(),
    receiver: Api.Receiver,
): Promise<any> => {
    headers.append("accept", receiver.accept || "*/*")
    const api: Api.Api = {
        args,
        method: "PUT",
        path,
        headers,
        receiver,
    }
    return createRequest(api)
}

export const putJSON = (
    path: string,
    args?: any,
    headers: Headers = new Headers(),
    receiver: Api.Receiver = ReceiveJSON,
): Promise<any> => {
    headers.append("Content-Type", "application/json; charset=UTF-8")
    return put(path, JSON.stringify(args), headers, receiver)
}

export const remove = (
    path: string,
    args?: any,
    headers: Headers = new Headers(),
    receiver: Api.Receiver = ReceiveNothing,
): Promise<any> => {
    const api: Api.Api = {
        args,
        headers,
        method: "DELETE",
        path,
        receiver,
    }
    return createRequest(api)
}

function createRequest(api: Api.Api): Promise<any> {
    const credentials = "same-origin"
    const { method, path, args } = api

    // We are using the localhost address to make sure that no other device on the same network can access the API
    let fullPath = "http://127.0.0.1:8081/" + path
    const headers = api.headers ?? new Headers()
    headers.append("accept", api.receiver.accept || "*/*")

    let resultPromise: Promise<any> = Promise.resolve()
    resultPromise = resultPromise.then(() => {
        if (args) {
            if (method !== "GET") {
                console.log(
                    `Calling API on ${fullPath} with payload: `,
                    args,
                )
                return fetch(fullPath, {
                    method,
                    headers,
                    body: args,
                    credentials,
                })
            }

            const queryParams = "&" +
                Object.entries(args)
                    .filter(
                        (arg: [string, any]) =>
                            !arg.contains(null) && !arg.contains(undefined),
                    )
                    .map((arg: [string, any]) =>
                        arg.map(encodeURIComponent).join("="),
                    )
                    .join("&")

            fullPath += queryParams.length > 1 ? queryParams : ""
        }
        console.log(`Calling API on ${fullPath} `)
        return fetch(fullPath, { method, headers, body: null, credentials })
    })
    return resultPromise.then(api.receiver.handler).catch((err) => {
        // This is where we should create a NetworkException and raise it in a future error handling rework
        if (err instanceof TypeError) {
            console.error("A network exception occured: " + err)
        }
        throw err
    })
}

export const defaultJSONParsing = (response: Response): any => {
    if (!response.ok) {
        throw response
    }
    if (response.status === 204) {
        return new Promise(function (resolve) {
            return resolve({})
        })
    }
    return response.json()
}

export const defaultTextParsing = (response: Response): any => {
    if (!response.ok) {
        throw response
    }
    if (response.status === 204) {
        return new Promise(function (resolve) {
            return resolve("")
        })
    }
    return response.text()
}

export const ReceiveJSON: Api.Receiver = {
    accept: "application/json",
    handler: defaultJSONParsing,
}
export const ReceiveXML: Api.Receiver = {
    accept: "text/xml",
    handler: defaultTextParsing,
}
export const ReceiveText: Api.Receiver = {
    accept: "*/*",
    handler: defaultTextParsing,
}
export const ReceiveNothing: Api.Receiver = {
    accept: "*/*",
    handler: (response: Response) => {
        if (!response.ok) {
            throw response
        }
    },
}
