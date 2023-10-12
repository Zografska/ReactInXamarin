import { Switch, Route, BrowserRouter } from "react-router-dom";

import ListPage from "pages/listpage/ListPage";
import './styles/Defaults.scss';

function App(): JSX.Element {
  console.log("hello")
  return (
    <BrowserRouter>
      <Switch>
        <Route path="/" component={ListPage} />
      </Switch>
    </BrowserRouter>
  );
}

export default App;
