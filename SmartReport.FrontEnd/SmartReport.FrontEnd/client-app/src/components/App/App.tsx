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
import { AppProps } from '../../types/component-types';
import {loadDefaultLocale} from '../../redux/actions/locales-actions'

const { Content, Footer } = Layout;


class App extends React.PureComponent<AppProps>{
  constructor(props: AppProps) {
    super(props);
    this.props.loadDefaultLocale();
  }

  render() {
    return (
      <BrowserRouter>
        <IntlProvider locale={this.props.locale} messages={this.props.messages}>
          <Layout>
            <HeaderContainer />
            <Content className="site-layout" style={{ padding: '0 50px', marginTop: 64 }}>
              <div className="site-layout-background" style={{ padding: 24, minHeight: 380 }}>
                <Switch>
                  <Route exact path="/" component={HomeContainer} />
                </Switch>
              </div>
            </Content>
            <Footer style={{ textAlign: 'center' }}>{new Date().getFullYear()}</Footer>
          </Layout>
        </IntlProvider>
      </BrowserRouter>
    )
  }
}

const mapStateToProps = (state: any) => {
  return {
    locale: state.intl.locale,
    messages: state.intl.messages
  }
};

const mapDispatchToProps = (dispatch: any) => {
  return bindActionCreators({
    loadDefaultLocale: loadDefaultLocale,
  }, dispatch)
}

const AppContainer = connect(mapStateToProps, mapDispatchToProps)(App);
export default AppContainer;
