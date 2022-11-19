import { useContext } from "react";
import { View, ActivityIndicator, StyleSheet } from "react-native";
import { LoadingContext } from "../context/LoadingContext";

export default function Loading() {
    const {isLoading} = useContext(LoadingContext);
    
    return (
        <View style={styles.loading}>
            <ActivityIndicator size={'large'} color={'#7ED957'} animating={isLoading} />
        </View>
    );
};

const styles = StyleSheet.create({
    loading: {
        position: 'absolute',
        left: 0,
        right: 0,
        top: 0,
        bottom: 0,
        alignItems: 'center',
        justifyContent: 'center'
    }
});