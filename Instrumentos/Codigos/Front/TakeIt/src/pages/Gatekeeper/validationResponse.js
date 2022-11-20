import { useContext, useEffect, useState } from "react";
import { StyleSheet, Text, View } from "react-native";
import axios from 'axios';
import Loading from "../../components/loading";
import { LoadingContext } from "../../context/LoadingContext";


export default function ValidationResponse({route}) {
    const qrCodeResponse = route.params;
    const [status, setStatus] = useState(null);
    const {setIsLoading} = useContext(LoadingContext);

    const validateTicketQrCode = async () => {
        try {
            setIsLoading(true);
            console.log('qrCode ' + qrCodeResponse);
            await axios.post("/api/v1/ticketvalidation/ticket", qrCodeResponse);
            console.log('sucesso');
            setStatus(true);
        } catch (err) {
            console.log(err);
            setStatus(false);
        } finally {
            setIsLoading(false);
        }
    };

    useEffect(() => {
        validateTicketQrCode();
    }, [])

    return (
        <View style={styles.container}>
            <Loading />
            <View>
                <Text>O resultado foi {status ? 'Sucesso' : 'Falha'}</Text>
            </View>
        </View>
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1
    }
});