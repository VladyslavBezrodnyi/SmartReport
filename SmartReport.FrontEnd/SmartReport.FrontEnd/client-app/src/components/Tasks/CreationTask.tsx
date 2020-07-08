import React from 'react';
import { Form, Input, Button, Modal } from 'antd';
import { bindActionCreators } from 'redux'
import { connect } from 'react-redux'
import { TaskCreationProps } from '../../types/props-types';
import { createTask } from '../../redux/actions/task-actions';
import { FormattedMessage } from 'react-intl';
import { TaskDTO } from '../../types/DTO-types';

const layout = {
  labelCol: { span: 8 },
  wrapperCol: { span: 16 },
};
const tailLayout = {
  wrapperCol: { offset: 8, span: 16 },
};

class CreationTask extends React.PureComponent<TaskCreationProps>{
  state = { visible: false };

  onFinish = (values: any) => {
    console.log('Success:', values);
    this.props.createTask(values as TaskDTO);
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
          <FormattedMessage id="admin.createTask" defaultMessage="Error!" />
        </Button>
        <Modal
          title={<FormattedMessage id="admin.createTask" defaultMessage="Error!" />}
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
              label={<FormattedMessage id="admin.createTask.title" defaultMessage="Error!" />}
              name="name"
              rules={[{ required: true, message: '' }]}
            >
              <Input />
            </Form.Item>

            <Form.Item
              label={<FormattedMessage id="admin.createTask.description" defaultMessage="Error!" />}
              name="description"
              rules={[{ required: true, message: '' }]}
            >
              <Input />
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
    createTask: createTask
  }, dispatch)
}

const CreationTaskContainer = connect(mapStateToProps, mapDispatchToProps)(CreationTask);
export default CreationTaskContainer;
