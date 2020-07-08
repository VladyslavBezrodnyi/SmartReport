import { notification } from 'antd';


const openNotification = (mess: string) => {
    notification.open({
      message: mess,
    });
  };

export default openNotification;