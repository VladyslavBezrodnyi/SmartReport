import { message } from 'antd';
import axios from 'axios';
import { environment } from './../../environment/environment';
import { ReportDTO } from '../../types/DTO-types';
import { GET_REPORT_BY_USER_ID } from '../../constants/report-constants';

export const createReport = (reportDTO: ReportDTO) => (despatch: any) => {
    return axios.put(environment.API_URL + '/api/Report/create', 
    {
        ...reportDTO
    },
    {
        headers: { "Authorization": `Bearer ${ localStorage.getItem('access_token')}` },
    }).then((response: any) => {
        message.success("Success!")
    }).catch((err: any) => {
        message.warning("Warning!");
        console.log(err);
    })
};

export const getReportByUserId = (userId: string) => (despatch: any) => {
    return axios.get(environment.API_URL + '/api/Report/GetByUserId/' + userId).then((response: any) => {
        despatch({
            type: GET_REPORT_BY_USER_ID,
            payload: {
                userReportsList: response.data
            }
        });
    }).catch((err: any) => {
        message.warning("Warning!");
        console.log(err);
    })
};