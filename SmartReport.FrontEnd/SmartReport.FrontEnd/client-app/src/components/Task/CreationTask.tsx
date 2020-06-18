import React from 'react';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import './App.css';
import { Layout } from 'antd';
import "antd/dist/antd.min.css";
import { bindActionCreators } from 'redux'
import { connect } from 'react-redux'
import HeaderContainer from '../Header/Header';
import { IntlProvider } from 'react-intl-redux'
import HomeContainer from '../Home/Home';
import { AppProps } from '../../types/props-types';


class CreationTask extends React.PureComponent<AppProps>{
  constructor(props: AppProps) {
    super(props);
  }

  render() {
    return (
     <div></div>
    );
  }
}

const mapStateToProps = (state: any) => {
  return {
  }
};

const mapDispatchToProps = (dispatch: any) => {
  return bindActionCreators({
  }, dispatch)
}

const CreationTaskContainer = connect(mapStateToProps, mapDispatchToProps)(CreationTask);
export default CreationTaskContainer;
