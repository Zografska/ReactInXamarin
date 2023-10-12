import React from 'react';
import { connect } from 'react-redux';
import * as queryString from 'query-string'; 

import { GlobalState } from '../../redux/global';

import { DispatchProps, mapDispatchToProps } from './actions';

import { LoadingPlaceholder } from './LoadingPlaceholder';


type StateProps = ListPage.ListPage;

type Props = StateProps & DispatchProps;

export class ListPage extends React.Component<Props> {
  observer: IntersectionObserver | null = null;
  loadingRef: HTMLDivElement | null = null;
  pullToRefreshPromise: Function | null = null;
  listId: string;

  constructor(props: Props) {
    super(props);
    const params = this.props as any;
    const queryParams = queryString.parse(params.location.search);
    this.listId = queryParams['listId'] as string;
  }

  componentDidMount(): void {
    this.loadPage(1, "")
    window.onReceiveMessage = this.onReceiveMessage.bind(this);
  }

  componentWillUnmount(): void {
    this.observer?.disconnect();
  }

  onReceiveMessage(message: Models.WebMessage) {

  }

  loadPage = (page: number, searchText: string) => {
    const { loadMoreItems } = this.props;

    loadMoreItems(this.listId, page, searchText, false);
  }

  render(): JSX.Element {
    const {
      items,
      isLoading,
    } = this.props;

    const isListEmpty = items.length === 0 && !isLoading;


    return (
      <div>
          <div style={{height:'100%', overflowY:'auto'}}>
            <input value="Hello List Page"></input>
            <div>
              {isListEmpty ? (
                  <div>
                  The list has no items
                </div>
                ) : (
                  items.map((item: Models.Item) => (
                    <div> {item.title} </div>)
                  )
                )
              } 
            </div>

            { isLoading ?? 
              <div>
                <LoadingPlaceholder />
                <LoadingPlaceholder />
                <LoadingPlaceholder />
                <LoadingPlaceholder />
              </div>
            }
          </div>
      </div>
    );
  }
}

const mapStateToProps = (state: GlobalState): StateProps => state.listPage;

export default connect(mapStateToProps, mapDispatchToProps)(ListPage);