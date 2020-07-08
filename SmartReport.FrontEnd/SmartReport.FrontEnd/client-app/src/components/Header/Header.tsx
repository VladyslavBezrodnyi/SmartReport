import React from 'react';
import { Layout, Menu, Button, Row, Col, Form, Modal, Input } from 'antd';
import "./Header.css";
import { HeaderProps } from '../../types/props-types';
import { Link, withRouter } from 'react-router-dom';
import { bindActionCreators } from 'redux'
import { connect } from 'react-redux'
import { login, logout } from '../../redux/actions/auth-actions';
import { LoginDTO } from '../../types/DTO-types';
import SwitchLocale from '../SwitchLocale/SwitchLocale'
import { FormattedMessage } from 'react-intl';

let layout = {
  labelCol: { span: 8 },
  wrapperCol: { span: 16 },
};
let tailLayout = {
  wrapperCol: { offset: 8, span: 16 },
};

class Header extends React.PureComponent<HeaderProps>{
  state = { visible: false };

  onFinish = (values: any) => {
    console.log('Success:', values);
    this.props.login({
      email: values.email,
      password: values.password
    } as LoginDTO);
    this.setState({
      visible: false,
    });
  };

  onFinishFailed = (errorInfo: any) => {
    console.log('Failed:', errorInfo);
  };

  loginButton = () => {
    return (
      <Col xs={8} xl={6}>
        <Button type="primary" size="middle" onClick={this.showModal}><FormattedMessage id="header.login" defaultMessage="Error!" /></Button>
      </Col>
    );
  }

  logOut1 = () => {
    this.props.logout();
    this.props.history.push(`/`)
  }
  logOutButton = () => {
    return (
      <Col xs={8} xl={6}>
        <Button type="primary" size="middle" onClick={this.logOut1}><FormattedMessage id="header.logout" defaultMessage="Error!" /></Button>
      </Col>
    );
  }

  showModal = () => {
    this.setState({
      visible: true,
    });
  }

  handleCancel = (e: any) => {
    console.log(e);
    this.setState({
      visible: false,
    });
  };

  getSwitchLocale = () => {
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
      return (
        <Col xs={16} xl={16}>
        </Col>);
    } else {
      return (
        <>
          <Col xs={4} xl={10}>
          </Col>
          <Col xs={12} xl={6}>
            <SwitchLocale />
          </Col>
        </>
      );
    }
  }

  render() {
    return (
      <div>
        <Layout.Header className="site-layout-background" style={{ padding: 0 }}>
          <Row>
            {this.getSwitchLocale()}
            {this.props.auth?.isAuthenticated ? this.logOutButton() : this.loginButton()}
          </Row>
        </Layout.Header>
        <Modal
          title={<FormattedMessage id="header.login" defaultMessage="Error!" />}
          visible={this.state.visible}
          footer={null}
          onCancel={this.handleCancel}
        >
          <Form
            {...layout}
            name="basic"
            initialValues={{ remember: true }}
            onFinish={this.onFinish}
            onFinishFailed={this.onFinishFailed}
          >
            <Form.Item
              label={<FormattedMessage id="email" defaultMessage="Error!" />}
              name="email"
              rules={[{ required: true, message: '' }]}
            >
              <Input />
            </Form.Item>

            <Form.Item
              label={<FormattedMessage id="password" defaultMessage="Error!" />}
              name="password"
              rules={[{ required: true, message: '' }]}
            >
              <Input.Password />
            </Form.Item>

            <Form.Item {...tailLayout}>
              <Button type="primary" htmlType="submit">
                <FormattedMessage id="submit" defaultMessage="Error!" />
              </Button>
            </Form.Item>
          </Form>
        </Modal>
      </div>
    );
  }
}

const mapStateToProps = (state: any) => {
  return {
    auth: state.auth
  }
};

const mapDispatchToProps = (dispatch: any) => {
  return bindActionCreators({
    login: login,
    logout: logout,
  }, dispatch)
}

const HeaderContainer = withRouter(connect(mapStateToProps, mapDispatchToProps)(Header as any));
export default HeaderContainer;