import { useNavigation } from "@react-navigation/native";
import { useContext, useEffect, useState } from "react";
import { StyleSheet, Text, View } from "react-native";
import axios from 'axios';
import { LoadingContext } from "../../context/LoadingContext";


export default function ValidationResponse({route}) {
    const navigation = useNavigation();
    const qrCodeResponse = route.params;
    const [status, setStatus] = useState(null);
    const {setIsLoading} = useContext(LoadingContext);

    const validateTicketQrCode = async () => {
        try {
            setIsLoading(true);
            let response = await axios.post("/api/v1/ticketvalidation/ticket/validate", {
                validationHash: qrCodeResponse
            });
            setStatus(response.data.valid);

            setTimeout(() => {
                navigation.navigate('Gatekeeper');
            }, 2000);
        } catch (err) {
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
            {status == null ? null : status ? <Success /> : <Error />}
        </View>
    );
};

const Success = () => {
    return (
        <View style={styles.containerResultSuccess}>
            <Text style={styles.textSuccess}>Válido!</Text>
        </View>
    );
};

const Error = () => {
    return (
        <View style={styles.containerResultError}>
            <Text style={styles.textError}>Inválido!</Text>
        </View>
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
    },
    containerResultSuccess: {
        flex: 1,
        backgroundColor: '#008000',
        alignItems: 'center',
        justifyContent: 'center'
    },
    textSuccess: {
        color: '#fff',
        fontSize: 55
    },
    containerResultError: {
        flex: 1,
        backgroundColor: '#ff0000',
        alignItems: 'center',
        justifyContent: 'center'
    },
    textError: {
        color: '#fff',
        fontSize: 55
    }
});