import ReactDOM from 'react-dom';
import store from 'redux/store';
import { Provider } from 'react-redux';
import createServer from "./mirage/server"
import App from './App';

if (process.env.NODE_ENV === "development"){
  createServer();
}

ReactDOM.render(
  <Provider store={store}>
    <App />
  </Provider>,
  document.getElementById('root')
);

declare global {
    interface Window {
        onReceiveMessage: Function;
    }
}