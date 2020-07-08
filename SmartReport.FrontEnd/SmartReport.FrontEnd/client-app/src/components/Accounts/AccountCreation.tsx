import React from 'react';
import { bindActionCreators } from 'redux'
import { connect } from 'react-redux'
import { AccountCreationProps } from '../../types/props-types';
import { register } from '../../redux/actions/auth-actions';
import { Form, Input, Button, Modal } from 'antd';
import { FormattedMessage } from 'react-intl';
import { RegistrationDTO } from '../../types/DTO-types';
import { getUsers } from '../../redux/actions/account-actions';

const layout = {
    labelCol: { span: 8 },
    wrapperCol: { span: 16 },
};
const tailLayout = {
    wrapperCol: { offset: 8, span: 16 },
};

class AccountCreation extends React.PureComponent<AccountCreationProps>{
    state = { visible: false };

    onFinish = (values: any) => {
        console.log('Success:', values);
        this.props.register(values as RegistrationDTO);
        this.setState({
            visible: false,
        });
    };

    onFinishFailed = (errorInfo: any) => {
        console.log('Failed:', errorInfo);
    };


    showModal = () => {
        this.setState({
            visible: true,
        });
    };

    handleCancel = (e: any) => {
        console.log(e);
        this.setState({
            visible: false,
        });
    };

    render() {
        return (
            <div>
                <Button type="primary" onClick={this.showModal}>
                    <FormattedMessage id="registration" defaultMessage="Error!" />
                </Button>
                <Modal
                    title={<FormattedMessage id="registration" defaultMessage="Error!" />}
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
                            label={<FormattedMessage id="name" defaultMessage="Error!" />}
                            name="name"
                            rules={[{ required: true, message: '' }]}
                        >
                            <Input />
                        </Form.Item>

                        <Form.Item
                            label={<FormattedMessage id="email" defaultMessage="Error!" />}
                            name="email"
                            rules={[{ required: true, message: '' }]}
                        >
                            <Input defaultValue={""} />
                        </Form.Item>

                        <Form.Item
                            label={<FormattedMessage id="password" defaultMessage="Error!" />}
                            name="password"
                            rules={[{ required: true, message: '' }]}
                        >
                            <Input/>
                        </Form.Item>

                        <Form.Item {...tailLayout}>
                            <Button type="primary" htmlType="submit">
                                {<FormattedMessage id="submit" defaultMessage="Error!" />}
                            </Button>
                        </Form.Item>
                    </Form >
                </Modal>
            </div>
        );
    }
}

const mapStateToProps = (state: any) => {
    return {
    }
};

const mapDispatchToProps = (dispatch: any) => {
    return bindActionCreators({
        register: register,
        getUsers: getUsers
    }, dispatch)
}

const AccountCreationContainer = connect(mapStateToProps, mapDispatchToProps)(AccountCreation);
export default AccountCreationContainer;
