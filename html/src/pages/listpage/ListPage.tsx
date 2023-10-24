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
  listId: string;

  constructor(props: Props) {
    super(props);
    const params = this.props as any;
    const queryParams = queryString.parse(params.location.search);
    this.listId = queryParams['listId'] as string;
  }

  componentDidMount(): void {
    this.props.loadMoreItems(this.listId);
    window.onReceiveMessage = this.onReceiveMessage.bind(this);
  }

  componentWillUnmount(): void {
    this.observer?.disconnect();
  }

  onReceiveMessage(message: Models.WebMessage) {
    if(message.Action === "SayHelloFromXamarin"){
      this.props.changeTitleColor(message.Data as string)
    }
  }

  onClick = (itemId: number) => {
    this.props.navigateToItemPage(itemId)
  }

  render(): JSX.Element {
    const {
      items,
      isLoading,
    } = this.props;

    const isListEmpty = items.length === 0 && !isLoading;


    return (
      <div>
          <div style={{height:'100%', overflowY:'auto', width:'auto', textAlign: 'center'}}>
          <h2>Hello from React!</h2>
            <div className="grid-container">
              {isListEmpty ? (
                <div className="empty-message">
                  <p>No items are present.</p>
                </div>
              
                ) : (
                  items.map((item: Models.Item) => (
                    <div className="grid-item" onClick={() => this.onClick(item.id)}> 
                    <div className="container">
                      <div className="info-text">{item.listName}</div>
                      <h2 className="title" style={{ color: this.props.titleColor }}>{item.title}</h2>
                      <p className="description">{item.description}</p>

                    </div>
                    </div>)
                  )
                )
              } 
            </div>
          
            {isLoading ?? 
              <div className="grid-container">
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