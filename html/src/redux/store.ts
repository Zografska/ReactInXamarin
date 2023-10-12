import {
    Store,
    StoreEnhancer,
    applyMiddleware,
    compose,
    createStore,
} from "redux"
import { rootEpic, rootReducer } from "./root"
import { composeWithDevTools } from "redux-devtools-extension"
import epicMiddleware from "./epicsDependencies"

// using the npm package for react dev tools: https://github.com/zalmoxisus/redux-devtools-extension#13-use-redux-devtools-extension-package-from-npm
// using the following recipe: https://github.com/zalmoxisus/redux-devtools-extension/blob/master/docs/Recipes.md#using-in-a-typescript-project
type WindowWithDevTools = Window & {
    // eslint-disable-next-line @typescript-eslint/ban-types
    __REDUX_DEVTOOLS_EXTENSION__: () => StoreEnhancer<unknown, {}>
}

const doesReduxDevToolsExtensionExist = (
    arg: Window | WindowWithDevTools,
): arg is WindowWithDevTools => {
    return "__REDUX_DEVTOOLS_EXTENSION__" in arg
}

let composeEnhancers = compose
if (!!doesReduxDevToolsExtensionExist) {
    composeEnhancers = composeWithDevTools({
        // Specify extension's options like name, actionsBlacklist, actionsCreators, serialize...
    })
}

function configureStore(): Store {
    const store = createStore(
        rootReducer,
        composeEnhancers(applyMiddleware(epicMiddleware)),
    )

    epicMiddleware.run(rootEpic)
    return store
}

const store = configureStore()
export default store
