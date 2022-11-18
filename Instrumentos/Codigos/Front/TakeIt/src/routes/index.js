import { createNativeStackNavigator } from '@react-navigation/native-stack';

import Welcome from '../pages/Welcome';
import SignIn from '../pages/SignIn';
import Events from '../pages/Events';
import Event from '../pages/Event';

const Stack = createNativeStackNavigator();

export default function Routes() {
    return (
        <Stack.Navigator>
            <Stack.Screen
                name="Welcome"
                component={Welcome}
                options={{ headerShown: false }}
            />

            <Stack.Screen
                name="SignIn"
                component={SignIn}
                options={{ headerShown: false }}
            />

            <Stack.Screen
                name="Events"
                component={Events}
                options={{ headerShown: true }}
            />

            <Stack.Screen
                name="Event"
                component={Event}
                options={{ headerShown: true }}
            />
        </Stack.Navigator>
    );
}

