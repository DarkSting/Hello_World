import axios from 'axios';

const Axios = axios.create({
    withCredentials:true,
    baseURL:'http://localhost:8080'
});

export default Axios;