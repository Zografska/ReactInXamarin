export const throttle = (fn: Function, delay = 2000) => {
    let lastCalled = 0;
    return (args: unknown) => {
        let now = new Date().getTime();
        if(now - lastCalled < delay) {
            return;
        }
        lastCalled = now;
        return fn(args);
    }
}