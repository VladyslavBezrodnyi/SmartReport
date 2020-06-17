import React from 'react';
import { Layout, Menu, Button, Row, Col, Form, Modal, Input } from 'antd';
import "./Header.css";
import { HeaderProps } from '../../types/component-types';
import { Link } from 'react-router-dom';
import { bindActionCreators } from 'redux'
import { connect } from 'react-redux'
import { registration, login, logout } from '../../redux/actions/auth-actions';
import { LoginDTO } from '../../types/account-types';
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
      <Col span={4} offset={2}>
        <Button type="primary" size="middle" onClick={this.showModal}><FormattedMessage id="header.login" defaultMessage="Error!" /></Button>
      </Col>
    );
  }

  logOutButton = () => {
    return (
      <Col span={4} offset={2}>
        <Button type="primary" size="middle" onClick={this.props.logout}><FormattedMessage id="header.logout" defaultMessage="Error!" /></Button>
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

  render() {
    return (
      <div>
        <Layout.Header className="header">
          <Row>
            <Col span={14} >
              <Menu theme="dark" mode="horizontal">
                <Menu.Item key="1" ><Link to="/"><FormattedMessage id="header.home" defaultMessage="Error!" /></Link></Menu.Item>
              </Menu>
            </Col>
            <Col span={4}>
              <SwitchLocale/>
            </Col>
            {this.props.auth?.isAuthenticated ? this.logOutButton() : this.loginButton()}
          </Row>
        </Layout.Header>
        <Modal
          title="LogIn"
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
              label="Username"
              name="email"
              rules={[{ required: true, message: 'Please input your username!' }]}
            >
              <Input />
            </Form.Item>

            <Form.Item
              label="Password"
              name="password"
              rules={[{ required: true, message: 'Please input your password!' }]}
            >
              <Input.Password />
            </Form.Item>

            <Form.Item {...tailLayout}>
              <Button type="primary" htmlType="submit">
                LogIn
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
    registration: registration,
  }, dispatch)
}

const HeaderContainer = connect(mapStateToProps, mapDispatchToProps)(Header as any);
export default HeaderContainer;