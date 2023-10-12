declare interface Array<T> {
    sum(): T
    merge(arr: Array<T>): Array<T>
    contains(item: T): boolean
    remove(elem: T): void
    move(from: number, to: number): void
}