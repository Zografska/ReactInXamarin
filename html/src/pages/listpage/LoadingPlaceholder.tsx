import React from 'react';

export class LoadingPlaceholder extends React.Component {
  render(): JSX.Element {
    return (
      <div className="loading-placeholder">
        <div className='wave'></div>
      </div>
    );
  }
}
