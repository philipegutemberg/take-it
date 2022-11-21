import { StyleSheet, Text, View, TouchableOpacity } from "react-native";
import * as Clipboard from 'expo-clipboard';
import { Entypo, Feather } from '@expo/vector-icons';
import axios from "axios";
import { useContext, useEffect, useState } from "react";
import { LoadingContext } from "../../context/LoadingContext";
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
            <View style={styles.qrCodeContainer}>
                <Text style={styles.textQrCode}>Endereço da sua wallet interna:</Text>
                {address ? 
                <QRCode
                    style={styles.qrCode}
                    size={250}
                    color='#7ED957'
                    backgroundColor='white'
                    value={address}
                />
                : null}
                <Text style={styles.addressText}>{address}</Text>
                <TouchableOpacity style={styles.copyButton} onPress={copyToClipboard}>
                    <Entypo name="copy" size={30} color='#7ED957' />
                </TouchableOpacity>
            </View>
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: 'white'
    },
    qrCodeContainer: {
        flex: 5,
        justifyContent: 'center',
        alignItems: 'center',
        alignSelf: 'center',
        // top: '2%'
    },
    textQrCode: {
        fontSize: 20,
        fontWeight: 'bold',
        bottom: '8%'
    },
    qrCode: {
        top: '5%'
    },
    addressText: {
        top: '5%',
        fontSize: 14,
        fontWeight: 'bold',
    },
    copyButton: {
        top: '7%'
    }
});