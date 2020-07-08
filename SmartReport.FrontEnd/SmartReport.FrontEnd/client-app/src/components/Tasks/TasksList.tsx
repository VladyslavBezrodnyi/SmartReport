import React from 'react';
import { bindActionCreators } from 'redux'
import { connect } from 'react-redux'
import { Divider } from 'antd';
import { FormattedMessage } from 'react-intl';
import { List } from 'antd';
import Text from 'antd/lib/typography/Text';
import { TasksListProps } from '../../types/props-types';
import CreationTaskContainer from './CreationTask';
import { TaskDTO } from '../../types/DTO-types';
import { getTasks } from '../../redux/actions/task-actions';

class TasksList extends React.PureComponent<TasksListProps>{
    constructor(props: TasksListProps) {
        super(props);
        this.props.getTasksList();
    }

    render() {
        return (
            <div>
                <CreationTaskContainer />
                <Divider><FormattedMessage id="header.tasks" defaultMessage="Error!" /></Divider>
                <List
                    itemLayout="horizontal"
                    dataSource={this.props.tasksList}
                    renderItem={(item: TaskDTO, i: number) => (
                        <List.Item>
                            <List.Item.Meta
                                title={<Text strong>{`${item.id} - ${item.name}`}</Text>}
                                description={
                                    <>
                                        <Text><FormattedMessage id="description" defaultMessage="Error!" />{": "}{item.description}</Text>
                                    </>
                                }
                            />
                        </List.Item>
                    )}
                />
            </div>
        )
    }

}

const mapStateToProps = (state: any) => {
    return {
        tasksList: state.task.tasksList
    }
};

const mapDispatchToProps = (dispatch: any) => {
    return bindActionCreators({
        getTasksList: getTasks
    }, dispatch)
}

const TasksListContainer = connect(mapStateToProps, mapDispatchToProps)(TasksList);
export default TasksListContainer;