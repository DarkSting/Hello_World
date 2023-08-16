import axios from 'axios';

const Axios = axios.create({
    withCredentials:true,
    baseURL:'http://localhost:8080',
    headers:{
        'Content-Type': 'application/json'
    }    
});

export default Axios;