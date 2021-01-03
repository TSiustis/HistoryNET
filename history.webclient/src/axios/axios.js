import axios from 'axios';

const instance = axios.create({
    baseURL: 'http://localhost:44350',
    headers:{
        headerType: 'example header type'
    }
});

export default instance;