import { StyleSheet, Text, View, TouchableOpacity } from "react-native";
import * as Clipboard from 'expo-clipboard';
import { Entypo, Feather } from '@expo/vector-icons';
import axios from "axios";
import { useContext, useEffect, useState } from "react";
import { LoadingContext } from "../../context/LoadingContext";
import Loading from "../../components/loading";
import QRCode from 'react-native-qrcode-svg';

export default function Wallet() {
    const {setIsLoading} = useContext(LoadingContext);
    const [address, setAddress] = useState(null);
    
    const getAddress = async () => {
        try {
            setIsLoading(true);
            let response = await axios.get('/api/v1/users/customer/address');
            setAddress(response.data.address);
        } catch (err) {
            console.error(err);
        } finally {
            setIsLoading(false);
        }
    };

    useEffect(() => {
        getAddress();
    }, [])

    const copyToClipboard = async () => {
        await Clipboard.setStringAsync(address);
    }

    return (
        <View style={styles.container}>
            <Loading />
            <View style={styles.qrCodeContainer}>
                <Text>Endereço da sua wallet interna:</Text>
                {address ? 
                <QRCode 
                    size={300}
                    color='black'
                    backgroundColor='white'
                    value={address}
                />
                : null}
            </View>
            <View>
            <Text>{address}</Text>
            <TouchableOpacity onPress={copyToClipboard}>
                <Entypo name="copy" />
            </TouchableOpacity>
            </View>
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1
    },
    qrCodeContainer: {
        flex: 4,
        alignItems: 'center',
        // top: '2%'
    },
});