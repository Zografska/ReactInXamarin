declare namespace Api {
    interface Api {
        method: string
        path: string
        headers?: Headers
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        args?: any
        receiver: Receiver
    }

    interface Receiver {
        accept: string
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        handler: (response: Response) => any
    }
}
