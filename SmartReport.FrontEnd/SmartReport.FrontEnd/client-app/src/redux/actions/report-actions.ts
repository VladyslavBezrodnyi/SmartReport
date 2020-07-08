import { message } from 'antd';
import axios from 'axios';
import { environment } from './../../environment/environment';
import { ReportDTO } from '../../types/DTO-types';

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