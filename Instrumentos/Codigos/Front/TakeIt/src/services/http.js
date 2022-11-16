import axios from 'axios'
import { ActionSheetIOS } from 'react-native';

const Http = {
    Get: async function(url) {
        return await axios.get(url);
    },
    Post: async function(url, body) {
        

        try {
            return await axios.post(url, body);
        } catch (error) {
            console.error(error);
        }
        
    }
};

export default Http;